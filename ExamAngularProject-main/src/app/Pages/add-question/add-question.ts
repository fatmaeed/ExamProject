import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { QuestionService } from '../../Services/question-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-question',
  imports: [ReactiveFormsModule],
  templateUrl: './add-question.html',
  styleUrl: './add-question.css'
})
export class AddQuestion {
  createQuestionForm = new FormGroup({
    question: new FormControl('' , [Validators.required]),
    option1: new FormControl('' , [Validators.required]),
    option2: new FormControl('' , [Validators.required]),
    option3: new FormControl('' , [Validators.required]),
    option4: new FormControl('' , [Validators.required]),
    score : new FormControl('1' , [Validators.required , Validators.min(1) , Validators.max(20)]),
    correctOption: new FormControl('' , [Validators.required]),
  });
  options:IOptionModelForForm[] = [
    {
      option : 'option1',
      label : 'A',
      isCorrect : false
    },
    {
      option : 'option2',
      label : 'B',
      isCorrect : false
    },
    {
      option : 'option3',
      label : 'C',
      isCorrect : false
    },
    {
      option : 'option4',
      label : 'D',
      isCorrect : true
    },

  ];
  getOption(AnswerOption: AnswerOption) {
    return this.createQuestionForm.controls[AnswerOption];
  }
  get getScore() {
    return this.createQuestionForm.controls['score'];
  }

  get getQuestion() {
    return this.createQuestionForm.controls['question'];
  }
  
  get getOption1() {
    return this.createQuestionForm.controls['option1'];
  }
  get getOption2() {
    return this.createQuestionForm.controls['option2'];
  }
  get getOption3() {
    return this.createQuestionForm.controls['option3'];
  }
  get getOption4() {
    return this.createQuestionForm.controls['option4'];
  }
  get getCorrectOption() {
    return this.createQuestionForm.controls['correctOption'];
  }


 constructor (private questionService :QuestionService ,
  private activatedRoute: ActivatedRoute ,
  private router : Router,
  private location : Location
){}

 createQuestionFunc(createQuestion : ICreateQuestionModel) {
   this.questionService.createQuestion(createQuestion).subscribe({
     next: (response) => {
      this.goBack();
      //  this.router.navigate([`/exam/${this.activatedRoute.snapshot.paramMap.get('id')}/questions`]);
     },
     error: (error) => {
       console.log(error);
     }
   } )
    
 }
 onFormSubmit() {
  if(this.createQuestionForm.invalid) return;
  const examId = this.activatedRoute.snapshot.paramMap.get('id');
  const createQuestion : ICreateQuestionModel = {
    examId : Number(examId),
    score : Number(this.getScore.value!),
    questionText : this.getQuestion.value!,
    choice1 : this.getOption1.value!,
    choice2 : this.getOption2.value!,
    choice3 : this.getOption3.value!,
    choice4 : this.getOption4.value!,
    correctAnswer : Number(this.getCorrectOption.value!)
  }
  this.createQuestionFunc(createQuestion);
 }
 goBack (){
  this.location.back();
 }
}
