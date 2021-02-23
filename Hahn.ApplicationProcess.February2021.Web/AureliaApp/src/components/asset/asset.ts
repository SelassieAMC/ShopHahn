import { BootstrapFormRenderer } from './../common/bootstrap-form-renderer';
import { AssetModel } from './../models/assetModel';
import { bindable } from 'aurelia-framework';
import {inject, NewInstance} from 'aurelia-dependency-injection';
import './asset.css';
import '../common/datepicker';
import { ValidationRules, ValidationController } from 'aurelia-validation';

@inject(NewInstance.of(ValidationController))
export class Asset {
  @bindable
  public assetObject = new AssetModel();
  constructor(private validationController: ValidationController){
    validationController.addRenderer(new BootstrapFormRenderer());
  }

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
      .ensure("name").required().withMessage("Asset name is required")
                     .minLength(5).withMessage("Asset name must have at least 5 characters.")
      .ensure("countryDep").required().withMessage("Country of department is required.")
      .ensure("emailDep").required().withMessage("Email address is required")
                         .email().withMessage("Email address must follow the @domain syntax.")
      .ensure("department").range(1,5).withMessage("Invalid department type.")
      .ensure("purchaseDate").satisfies(x => new Date(x) > lessOneYear).withMessage("The purchase date must not be older than one year.")
      .on(this.assetObject);
  }

  public submit() {
    this.validationController.validate().then( 
      x => {
        if (x.valid) {
          alert("Validation successful!");
          //send data to api
        } else {
          alert("Validation failed!");
        }
      })
    
  }
}
  