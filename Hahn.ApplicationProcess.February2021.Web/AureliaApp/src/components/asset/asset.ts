import { PLATFORM } from 'aurelia-pal';
import { BootstrapFormRenderer } from './../common/bootstrap-form-renderer';
import { AssetModel } from './../models/assetModel';
import { bindable } from 'aurelia-framework';
import {inject, NewInstance} from 'aurelia-dependency-injection';
import './asset.css';
import '../common/datepicker';
import { ValidationRules, ValidationController } from 'aurelia-validation';
import {AssetService} from '../../services/assetService';
import {DialogService} from 'aurelia-dialog';
import {Dialog} from '../common/dialog/dialog';
import { I18N } from 'aurelia-i18n';
import { Router } from 'aurelia-router';

@inject(NewInstance.of(ValidationController), AssetService, DialogService, I18N, Router)
export class Asset {
  
  constructor(
    private validationController: ValidationController, 
    private assetService: AssetService, 
    private dialogService:DialogService, 
    private i18n:I18N, 
    private router:Router){
    validationController.addRenderer(new BootstrapFormRenderer());
    this.dialogService = dialogService;
    this.i18n = i18n;
    this.router = router;
  }
  @bindable
  public assetObject = new AssetModel();
  public departments = [
    {id:1, name: this.i18n.tr('assetForm.departmentTypes.HQ')},
    {id:2, name: this.i18n.tr('assetForm.departmentTypes.Store1')},
    {id:3, name: this.i18n.tr('assetForm.departmentTypes.Store2')},
    {id:4, name: this.i18n.tr('assetForm.departmentTypes.Store3')},
    {id:5, name: this.i18n.tr('assetForm.departmentTypes.MaintenanceStation')}
  ];

  public bind(){
    let lessOneYear = new Date();
    lessOneYear.setUTCFullYear(lessOneYear.getUTCFullYear() - 1);

    ValidationRules
      .ensure("assetName").required().withMessage(this.i18n.tr('assetForm.validationErrors.nameRequired'))
                     .minLength(5).withMessage(this.i18n.tr('assetForm.validationErrors.nameMinLength'))
      .ensure("countryOfDepartment").required().withMessage(this.i18n.tr('assetForm.validationErrors.countryRequired'))
      .ensure("eMailAdressOfDepartment").required().withMessage(this.i18n.tr('assetForm.validationErrors.emailRequired'))
                         .email().withMessage(this.i18n.tr('assetForm.validationErrors.emailSyntax'))
      .ensure("department").range(1,5).withMessage(this.i18n.tr('assetForm.validationErrors.departmentType'))
      .ensure("purchaseDate").satisfies(x => new Date(x) > lessOneYear).withMessage(this.i18n.tr('assetForm.validationErrors.purchaseDate'))
      .on(this.assetObject);
  }

  get canReset(){
    return this.assetObject.AnyDataFiled();
  }

  public reset(){
    this.showDialog(this.i18n.tr('confirmReset'),this.i18n.tr('confirmDialogTitle'),true,() => {
      this.validationController.removeObject(this.assetObject);
      this.assetObject = new AssetModel();
      this.bind();
    });
  }

  //@computedFrom('assetObject.name','assetObject.countryDep','assetObject.emailDep','assetObject.department')
  get canCreate(){
    return this.validationController.errors.length === 0 && this.assetObject.AllDataFiled()
  }


  public submit() {
    this.validationController.validate().then( 
      x => {
        if (x.valid) {
          this.assetService.SaveAsset(this.assetObject)
          .then(response => {
            if(response?.endOnSuccess){
              this.router.navigateToRoute("Success");
            }else{
              this.showDialog(response?.errorMessage,this.i18n.tr('errorAPI'))
            }
          })
          .catch(error => {
            this.showDialog(error,this.i18n.tr('errorAPI'))
          })
        }
      });
  }

  showDialog(message, title, confirmation=false, action?, cancelAction?){
    this.dialogService.open({ 
      viewModel: PLATFORM.moduleName('components/common/dialog/dialog'), 
      model:{ message : message, 
              title: title, 
              confirmation: confirmation,
              action: action,
              cancelAction: cancelAction}         
    })
    .then(response => {
      console.log(response);
    });
  }
}
  