import { SimpleValidationRenderer } from './components/common/simpleValidationRenderer';
import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import { I18N, TCustomAttribute } from 'aurelia-i18n';
import Backend from "i18next-xhr-backend";

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.use 
    .plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => { 
      const aliases = ['t', 'i18n']; 
      TCustomAttribute.configureAliases(aliases); 
      instance.i18next.use(Backend); 
      return instance.setup({ 
        backend: { 
          loadPath: './locales/{{lng}}/{{ns}}.json'
        }, 
        preload: ['en','es'],
        attributes: aliases, 
        lng : 'en', 
        fallbackLng : 'en',
          debug : false 
      }); 
    })
    .plugin(PLATFORM.moduleName('aurelia-validation'));

  // aurelia.container.registerHandler("simple-renderer",container => container.get(SimpleValidationRenderer));
  
  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
