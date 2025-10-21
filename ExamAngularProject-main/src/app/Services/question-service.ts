import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  baseUrl : string = "https://localhost:7085/api/";

  constructor(private httpClient:HttpClient) { }

   getAllQuestion(examId : number  , page :number = 1 , size : number = 10) : Observable<IQuestionModel []>{
    return this.httpClient.get(`${this.baseUrl}Admin/Exam/${examId}/Questions?page=${page}&size=${size}`) as Observable<IQuestionModel[]>;
  }
  updateQuestion(questionId :number , questionModel:IQuestionModel): Observable<IQuestionModel>{
    return this.httpClient.put(`${this.baseUrl}Admin/Question/${questionId}` , questionModel) as Observable<IQuestionModel> ;
  }
  deleteQuestion(questionId :number ) : Observable<IQuestionModel> {
    return this.httpClient.delete(`${this.baseUrl}Admin/Question/${questionId}`) as Observable<IQuestionModel> ;
  }
  createQuestion (questionModel : ICreateQuestionModel) : Observable<IQuestionModel> {
    return this.httpClient.post(`${this.baseUrl}Admin/Exam/${questionModel.examId}` , questionModel) as  Observable<IQuestionModel>;
  }
  searchAboutQuestions(examId:number , searchString : string) {
    return this.httpClient.get(`${this.baseUrl}Exam/${examId}/Questions?search=${searchString}`) as Observable<IQuestionModel[]>;
  }
  getQuestion(questionId : number) : Observable<IQuestionModel> {
    return this.httpClient.get(`${this.baseUrl}Admin/Question/${questionId}`) as Observable<IQuestionModel>;
  }
  searchAboutQuestion(examId : number , searchString : string , page :number = 1 , size : number = 10) : Observable<IQuestionModel[]> {
    return this.httpClient.get(`${this.baseUrl}Admin/Exam/${examId}/Questions/${searchString}?page=${page}&pageSize=${size}`) as Observable<IQuestionModel[]>;
  }
}
