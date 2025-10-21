import { ActivatedRoute, RouterLink } from '@angular/router';
import { QuestionService } from './../../Services/question-service';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { QuestionsList } from "../questions-list/questions-list";
import { PaginationBar } from "../../Components/pagination-bar/pagination-bar";
import { ExamService } from '../../Services/ExamService/exam-service';
import { LoadingSpinner } from "../../Components/loading-spinner/loading-spinner";
import { delay } from 'rxjs';
import { CommonModule } from '@angular/common';
import { SearchBar } from "../../Components/search-bar/search-bar";

@Component({
  selector: 'app-exam-questions',
  imports: [QuestionsList, RouterLink, PaginationBar, LoadingSpinner, CommonModule, SearchBar],
  templateUrl: './exam-questions.html',
  styleUrl: './exam-questions.css'
})
export class ExamQuestions implements OnInit{
isExamLoaded : boolean = false;
isQuestionsLoaded : boolean = false;
examId! : number;
pages! : number[];
selectedPage : number = 1;
examInfoModel! :IExamInfo ;
cachedQuestions: {page : number , questions : IQuestionModel[] }[] =[] ;
currentQuestions! : IQuestionModel[] ;

constructor( private questionService :QuestionService , 
  private activatedRoute: ActivatedRoute ,
  private examService : ExamService 
 , private changeDetectorRef: ChangeDetectorRef
) {
  this.examId =Number(this.activatedRoute.snapshot.paramMap.get('id')!);

}

cashQuestions(page : number , questions : IQuestionModel[]) {
  this.cachedQuestions.push({page , questions});
}
getIfCashed(page : number) {
  return this.cachedQuestions.find(cachedPage => cachedPage.page === page);
}

ngOnInit(): void {
  this.GetExam();
  this.GetQuestions();
 }

  private GetExam() {
    this.examService.gEtExam(this.examId).pipe(delay(500)).subscribe({
      next: (response) => {
        this.examInfoModel = response;
        this.pages = Array.from({
           length: Math.ceil(this.examInfoModel.numberOfQuestions / 10) },
            (_, i) => i + 1);
          this.isExamLoaded = true;
          this.changeDetectorRef.detectChanges();

     
      },
      error: (error) => {
        console.log(error);
        this.isExamLoaded = true;
        this.changeDetectorRef.detectChanges();
      }
    });
  }

  private GetQuestions() {
    this.questionService.getAllQuestion(this.examId).subscribe({
      next: (response) => {
        this.cashQuestions(1, response);
        this.currentQuestions = response;
        console.log(this.currentQuestions);
        this.isQuestionsLoaded = true;
        this.changeDetectorRef.detectChanges();


      },
      error: (error) => {
        console.log(error);
        this.isQuestionsLoaded = true;
        this.changeDetectorRef.detectChanges();

      }
    });
  }
  removeQuestionFromList(question : IQuestionModel) {
    this.currentQuestions = this.currentQuestions.filter(q => q.id !== question.id);
    //delete from cache
    this.cachedQuestions = this.cachedQuestions.filter(cachedPage => cachedPage.questions.some(q => q.id !== question.id));
    this.updateExamInfo(question);
    this.changeDetectorRef.detectChanges();
  }

  private updateExamInfo(question: IQuestionModel) {
    this.examInfoModel.numberOfQuestions -= 1;
    this.examInfoModel.totalScore -= question.score;
  }

 changePage(page : number) {
  this.selectedPage = page;
  const cachedPage = this.getIfCashed(page);
  if (cachedPage) {
    this.currentQuestions = cachedPage.questions;
  } else {
    this.isQuestionsLoaded = false;
    this.questionService.getAllQuestion(this.examId , page).pipe(delay(500)).subscribe({
      next: (response) => {
        this.cashQuestions(page , response);
        this.currentQuestions = response;
        this.isQuestionsLoaded = true;
        this.changeDetectorRef.detectChanges();
      },
      error: (error) => {
        console.log(error);
        this.isQuestionsLoaded = true;
        this.changeDetectorRef.detectChanges();
      }
    });
  }
 }
 searchAboutQuestion(searchString : string | null) {
  if(!searchString) {
    this.changePage(this.selectedPage);
    return;
  };
  this.questionService.searchAboutQuestion(this.examId , searchString).subscribe({
    next: (response) => {
      this.currentQuestions = response;
      this.changeDetectorRef.detectChanges();
    },
    error: (error) => {
      console.log(error);
      this.changeDetectorRef.detectChanges();
    }
  });
 }
}



