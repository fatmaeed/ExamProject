import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-empty-card',
  imports: [],
  templateUrl: './empty-card.html',
  styleUrl: './empty-card.css'
})
export class EmptyCard {
@Input({required : true}) emptyModel! : {title : string , description : string}
}
