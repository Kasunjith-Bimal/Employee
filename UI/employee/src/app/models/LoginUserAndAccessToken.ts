import { Token } from "@angular/compiler";

export interface LoginUserAndAccessToken {
    Email : string,
    FullName : string,
    IsFirstLogin : boolean,
    TokenDetail :Token
  }