import { Component, EventEmitter, Input , Output,  } from '@angular/core';
import { QuestionCard } from "./question-card/question-card";
import { EmptyCard } from "../../Components/empty-card/empty-card";

@Component({
  selector: 'app-questions-list',
  imports: [QuestionCard, EmptyCard],
  templateUrl: './questions-list.html',
  styleUrl: './questions-list.css'
})
export class QuestionsList {
  @Input({required : true}) displayedQuestions! : IQuestionModel[]  ; 
  @Input({required : true}) page! : number ;
  @Output () deleteQuestion = new EventEmitter<IQuestionModel>()

  constructor() { }
  
  removeQuestion(question : IQuestionModel) {
    this.displayedQuestions.splice(this.displayedQuestions.indexOf(question), 1);
    this.deleteQuestion.emit(question);
  }
 

}
