import { Post } from './Post';
export class Favorite {
  public Id?:number;
  public UserId?:number;
  public PostId?:number;
  public DateAdded?:Date;
  public Post?:Post;
}
