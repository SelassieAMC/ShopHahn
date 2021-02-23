import { RenderInstruction } from 'aurelia-validation';

export class SimpleValidationRenderer {
  public render(instruction: RenderInstruction) {
    for (let {result, elements} of instruction.unrender) {
      elements.forEach(target => {
        target.parentElement.querySelector(".error").textContent = ""
        //target.
      });
    }
    for (let {result, elements} of instruction.render) {
      elements.forEach(target => target.parentElement.querySelector(".error").textContent = result.message);
    }
  }
}
