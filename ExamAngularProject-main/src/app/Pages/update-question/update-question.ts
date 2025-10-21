import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../../Services/question-service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-update-question',
  imports: [ReactiveFormsModule],
  templateUrl: './update-question.html',
  styleUrl: './update-question.css'
})
export class UpdateQuestion implements OnInit {
  question! :IQuestionModel ;
  constructor(private questionService :QuestionService ,
     private activatedRoute: ActivatedRoute ,
    private location : Location,
  
  ) { }
  ngOnInit(): void {
    this.questionService.getQuestion(Number(this.activatedRoute.snapshot.paramMap.get('questionId')!)).subscribe({
      next: (response) => {
        this.question = response;
        this.getQuestion.setValue(this.question.questionText);
        this.getOption1.setValue(this.question.choice1);
        this.getOption2.setValue(this.question.choice2);
        this.getOption3.setValue(this.question.choice3);
        this.getOption4.setValue(this.question.choice4);
        this.getScore.setValue(String(this.question.score));
        this.getCorrectOption.setValue(this.question.correctAnswer);
      },
      error: (error) => {
        console.log(error);
      }
    } )}

    UpdateQuestionForm = new FormGroup({
      question: new FormControl("" , [Validators.required]),
      option1: new FormControl("" , [Validators.required]),
      option2: new FormControl("" , [Validators.required]),
      option3: new FormControl("", [Validators.required]),
      option4: new FormControl("", [Validators.required]),
      score : new FormControl("" , [Validators.required , Validators.min(1) , Validators.max(20)]),
      correctOption: new FormControl(0 , [Validators.required]),
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

    updateQuestionFunc(questionId :number , questionModel : IQuestionModel) {
      this.questionService.updateQuestion( questionId , questionModel).subscribe({
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
      if(this.UpdateQuestionForm.invalid) return;
      const examId = this.activatedRoute.snapshot.paramMap.get('id');
      const questionModel : IQuestionModel = {
       id : Number(this.activatedRoute.snapshot.paramMap.get('questionId')),
         examId : Number(examId),
        score : Number(this.getScore.value!),
        questionText : this.getQuestion.value!,
        choice1 : this.getOption1.value!,
        choice2 : this.getOption2.value!,
        choice3 : this.getOption3.value!,
        choice4 : this.getOption4.value!,
        correctAnswer : Number(this.getCorrectOption.value!)
      }
      this.updateQuestionFunc(questionModel.id , questionModel);
     }



    //  get Controllers
    getOption(AnswerOption: AnswerOption) {
      return this.UpdateQuestionForm.controls[AnswerOption];
    }
    get getScore() {
      return this.UpdateQuestionForm.controls['score'];
    }
  
    get getQuestion() {
      return this.UpdateQuestionForm.controls['question'];
    }
    
    get getOption1() {
      return this.UpdateQuestionForm.controls['option1'];
    }
    get getOption2() {
      return this.UpdateQuestionForm.controls['option2'];
    }
    get getOption3() {
      return this.UpdateQuestionForm.controls['option3'];
    }
    get getOption4() {
      return this.UpdateQuestionForm.controls['option4'];
    }
    get getCorrectOption() {
      return this.UpdateQuestionForm.controls['correctOption'];
    }


    goBack (){
      this.location.back();
     }
  
  
}
