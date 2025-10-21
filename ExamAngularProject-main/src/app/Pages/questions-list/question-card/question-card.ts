import {  Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { OptionCard } from "./option-card/option-card";
import { RouterLink } from '@angular/router';
import { QuestionService } from '../../../Services/question-service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-question-card',
  imports: [OptionCard , RouterLink],
  templateUrl: './question-card.html',
  styleUrl: './question-card.css'
})
export class QuestionCard implements OnInit {
  options : IOptionModel[] = [];
 @Input({required : true}) questionModel! : IQuestionModel ;
 @Input({required : true}) index! : number ;
 @Output () deleteQuestion = new EventEmitter<IQuestionModel>();
 constructor (private questionService : QuestionService){

 }
  ngOnInit(): void {
    this.options.push({option : this.questionModel.choice1 , label : "a" , isCorrect : this.questionModel.correctAnswer == 1})
    this.options.push({option : this.questionModel.choice2 , label : "b" , isCorrect : this.questionModel.correctAnswer == 2})
    this.options.push({option : this.questionModel.choice3 , label : "c" , isCorrect : this.questionModel.correctAnswer == 3})
    this.options.push({option : this.questionModel.choice4 , label : "d" , isCorrect : this.questionModel.correctAnswer == 4})
  }
  confirmDelete() {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to delete this question?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',

      reverseButtons: true,
    }).then((result) => {
      if (result.isConfirmed) {
        this.removeQuestion();
      }
    });
  }


  removeQuestion(){

    this.questionService.deleteQuestion(this.questionModel.id).subscribe(
      {
        next: (response) => {
          this.deleteQuestion.emit(this.questionModel);
          Swal.fire('Deleted!', `The question with id ${response.id} has been deleted.`, 'success');
        },
        error: (error) => {
          Swal.fire('Error!', 'Failed to delete the question.', 'error');
        }
      }
    );
  }


}
