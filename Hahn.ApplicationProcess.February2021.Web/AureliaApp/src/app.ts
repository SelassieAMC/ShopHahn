import { PLATFORM } from 'aurelia-pal';
require('bootstrap/dist/css/bootstrap.min.css');
require('bootstrap');
import 'app.css';
import { Router, RouterConfiguration } from 'aurelia-router';

export class App {
  router: Router;
  
    configureRouter(config: RouterConfiguration, router: Router): void {
      config.title = 'Aurelia';
      config.map([
        { route: 'creation', name: 'Success', moduleId: PLATFORM.moduleName('components/creation/success'),nav:true, title:'Success Creation'},
        { route: '', name: 'Asset', moduleId: PLATFORM.moduleName('components/asset/asset'), nav:true, title:'Asset Creation' }
      ]);
      this.router = router;
    }
}
