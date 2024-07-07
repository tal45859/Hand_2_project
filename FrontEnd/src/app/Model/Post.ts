import { Area } from "./Area";
import { Subcategory } from "./Subcategory";
import { User } from "./User";

export class Post {
  public Id?:number;
  public UserId?:number;
  public SubcategoryId?:number;
  public AreaId?:number;
  public Title?:string;
  public Body?:string;
  public Price?:string;
  public UploadDate?:Date;
  public NumberOfViews?:number;
  public User?:User;
  public Subcategory?:Subcategory;
  public Area?:Area;
}
