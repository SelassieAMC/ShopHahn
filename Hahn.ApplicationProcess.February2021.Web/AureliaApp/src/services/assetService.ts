import {HttpClient, json} from 'aurelia-fetch-client';
import {inject} from 'aurelia-framework';

@inject(HttpClient)
export class AssetService{
    constructor(private http: HttpClient){
        this.http = http;
        const baseUrl = 'http://localhost:5000/api/';
        http.configure(config => config.withBaseUrl(baseUrl));
    }

    SaveAsset(assetObject){
        return this.http.fetch('asset', {
            method: 'post',
            body: json(assetObject)
        })
        .then(response => response.json())
        .then(createdAsset => {
            return createdAsset;
        })
    }
}
