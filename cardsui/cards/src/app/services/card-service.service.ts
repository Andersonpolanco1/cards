import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Card } from 'src/models/card';

@Injectable({
  providedIn: 'root'
})
export class CardServiceService {

  constructor(private http : HttpClient) { }

  APIURL: string ="https://localhost:7067/api/Cards";
  

  getCards() : Observable<Card[]>{
    return this.http.get<Card[]>(this.APIURL);
  }

  postCard(card:Card) : Observable<Card>{
    card.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Card>(this.APIURL, card);
  }

  deleteCard(id:string) : Observable<Card>{
    return this.http.delete<Card>(this.APIURL.concat("/",id));
  }

  getCard(id:string) : Observable<Card>{
    return this.http.get<Card>(this.APIURL.concat("/", id));
  }


}
