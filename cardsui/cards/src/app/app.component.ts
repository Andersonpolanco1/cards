import { Component, OnInit } from '@angular/core';
import { Card } from 'src/models/card';
import { CardServiceService } from './services/card-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'cards';
  p: number = 1;
  collection: Card[] = [];
  card: Card = {
    id: '',
    cardHolderName: '',
    cardNumber: '',
    expiryMonth: '',
    expiryYear: '',
    cvc: ''
  };

  constructor(private cardService:CardServiceService){}
  ngOnInit(): void {
    this.getCards();
  }

  onSubmit(){
    this.cardService.postCard(this.card).subscribe(() => this.getCards());
    this.resetModel();
  }

  deleteCard(id:string){
    this.cardService.deleteCard(id).subscribe(() => this.getCards());
  }

  getCards(){
    this.cardService.getCards()
      .subscribe(r => this.collection = r);
  }

  editCard(card:Card){
    this.card = card;
  }
  resetModel(): void{
    this.card = {
      id: '',
      cardHolderName: '',
      cardNumber: '',
      expiryMonth: '',
      expiryYear: '',
      cvc: ''
    }
  }
}
