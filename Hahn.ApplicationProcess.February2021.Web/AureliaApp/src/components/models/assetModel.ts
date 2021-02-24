export class AssetModel {
  /**
   *
   */
  constructor() {
    this.id = 0,
    this.assetName = "",
    this.countryOfDepartment = "",
    this.eMailAdressOfDepartment = "",
    this.department = 0,
    this.isBroken = false
  }
  
  id: number;
  assetName: string;
  department: number;
  countryOfDepartment: string;
  eMailAdressOfDepartment: string;
  purchaseDate: Date;
  isBroken:boolean;

  public AllDataFiled(){
    let result = this.assetName !== "" &&
    this.countryOfDepartment !== "" &&
    this.eMailAdressOfDepartment !== "" &&
    this.department !== 0 &&
    this.purchaseDate !== undefined
    return result;
  }

  public AnyDataFiled(){
    let result = this.assetName !== "" ||
    this.countryOfDepartment !== "" ||
    this.eMailAdressOfDepartment !== "" ||
    this.department !== 0 ||
    this.purchaseDate !== undefined
    return result;
  }
}
