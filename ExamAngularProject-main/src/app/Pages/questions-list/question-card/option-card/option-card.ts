import { Component, Input } from '@angular/core';
@Component({
  selector: 'app-option-card',
  imports: [],
  templateUrl: './option-card.html',
  styleUrl: './option-card.css'
})
export class OptionCard {
 @Input({required: true}) optionModel! : IOptionModel ;

}
