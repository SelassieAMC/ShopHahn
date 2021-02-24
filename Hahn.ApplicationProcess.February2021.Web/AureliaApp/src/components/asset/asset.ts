import { BootstrapFormRenderer } from './../common/bootstrap-form-renderer';
import { AssetModel } from './../models/assetModel';
import { bindable } from 'aurelia-framework';
import {inject, NewInstance} from 'aurelia-dependency-injection';
import './asset.css';
import '../common/datepicker';
import { ValidationRules, ValidationController } from 'aurelia-validation';
import {computedFrom} from 'aurelia-framework';
import {AssetService} from '../../services/assetService';
import {DialogService} from 'aurelia-dialog';
import {Dialog} from '../common/dialog/dialog';

@inject(NewInstance.of(ValidationController), AssetService, DialogService)
export class Asset {
  
  constructor(private validationController: ValidationController, private assetService: AssetService, private dialogService:DialogService){
    validationController.addRenderer(new BootstrapFormRenderer());
    this.dialogService = dialogService;
  }
  @bindable
  public assetObject = new AssetModel();
  public departments = [
    {id:1, name: "HQ"},
    {id:2, name: "Store1"},
    {id:3, name: "Store2"},
    {id:4, name: "Store3"},
    {id:5, name: "Maintenance Station"}
  ];

  public bind(){
    let lessOneYear = new Date();
    lessOneYear.setUTCFullYear(lessOneYear.getUTCFullYear() - 1);

    ValidationRules
      .ensure("assetName").required().withMessage("Asset name is required")
                     .minLength(5).withMessage("Asset name must have at least 5 characters.")
      .ensure("countryOfDepartment").required().withMessage("Country of department is required.")
      .ensure("eMailAdressOfDepartment").required().withMessage("Email address is required")
                         .email().withMessage("Email address must follow the @domain syntax.")
      .ensure("department").range(1,5).withMessage("Invalid department type.")
      .ensure("purchaseDate").satisfies(x => new Date(x) > lessOneYear).withMessage("The purchase date must not be older than one year.")
      .on(this.assetObject);
  }

  public isEqual(obj1, obj2) {
    for (let prop in obj1) {
      if (typeof obj1[prop] === 'object') {
        return this.isEqual(obj1[prop], obj2[prop]);
      }
  
      if (obj1[prop] !== obj2[prop]) {
        return false;
      }
    }
    return true;
  }

  get canReset(){
    return this.assetObject.AnyDataFiled();
  }

  public reset(){
    this.showDialog("Are you really sure to reset all the data?","Confirmation",true,() => {
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
          //call http
          this.assetService.SaveAsset(this.assetObject)
          .then(response => {
            if(response?.endOnSuccess){
              //redirect to the page confirming
            }else{
              this.showDialog(response?.errorMessage,"Error Calling API")
            }
          })
          .catch(error => {
            this.showDialog(error,"Error Calling API")
          })
        }
      });
  }

  showDialog(message, title, confirmation=false, action?, cancelAction?){
    this.dialogService.open({ 
      viewModel: Dialog, 
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
  