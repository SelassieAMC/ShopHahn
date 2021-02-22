import {autoinject} from 'aurelia-dependency-injection';
import {ValidationControllerFactory, ValidationController} from 'aurelia-validation';
  
  @autoinject
  export class RegistrationForm {
    controller: ValidationController;
  
    constructor(controllerFactory: ValidationControllerFactory) {
      this.controller = controllerFactory.createForCurrentScope();
    }
  }
