# Hahn Application Process February 2021

Project made in .Net 5.0 API and Aurelia IO as SPA Framework

## Getting Started

The app is ready to launch as a container, once it is up, navigate to http://localhost:5010 you will be redirected to the Aurelia App, for swagger endpoint go to http://localhost:5010/swagger

Run and build

```shell
docker-compose up --build
```

You can download the image builded

```shell
docker pull selassieamc/hahnapplicationprocessfebruary2021web:latest
```
### Prerequisities


In order to run this container you'll need docker installed.

* [Windows](https://docs.docker.com/windows/started)
* [OS X](https://docs.docker.com/mac/started/)
* [Linux](https://docs.docker.com/linux/started/)

### Usage

#### Useful File Locations

* `docker-compose.override.yml` - Change port binding as you want
 
* `Hahn.ApplicationProcess.February2021.Web/AureliaApp/src/services/assetService.ts` - If you change the ports, change the base Url
///
## Built With

* .Net 5.0
* Swashbuckle 6 with Request and Response examples
* Entity Framework Core
* Aurelia io 1.3.1

## Find me

* [GitHub](https://github.com/SelassieAMC)
* [LinkedIn](https://www.linkedin.com/in/amc-dev/)
