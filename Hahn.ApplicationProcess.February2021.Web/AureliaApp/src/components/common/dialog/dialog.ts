import {DialogController} from 'aurelia-dialog';
import { autoinject } from 'aurelia-framework';
 
@autoinject
export class Dialog {    
    title?: string;
    message?: string;
    confirmation: boolean;
    action?: (args?: any) => {};
    cancelAction?: (args?: any) => {};
 
    constructor(private dialogController : DialogController) {
        dialogController.settings.centerHorizontalOnly = true;
    }
 
    activate(model : any) {
        this.message = model.message;
        this.title = model.title;
        this.confirmation = model.confirmation;
        this.action = model.action;
        this.cancelAction = model.cancelAction;
     }
 
     ok() : void {
        if(this.action !== undefined){
            this.action();
        } 
        this.dialogController.ok();
     }

     cancel() : void {
        if(this.cancelAction !== undefined){
            this.cancelAction();
        }
        this.dialogController.ok();
     }

    get isConfirmation(){
         return this.confirmation;
     }
}
