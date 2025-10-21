import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { QuizQuestion } from '../../Models/quiz-question';
import { HttpClient } from '@angular/common/http';
import { ISubmitionExam } from '../../Models/isubmition-exam';

@Injectable({ providedIn: 'root' })
export class QuizService {
  baseUrl:string = 'https://localhost:7085/api/'
  constructor(private http:HttpClient) {}

getExamQuestions(examId:string):Observable<any> {
  return this.http.get<any>(`${this.baseUrl}Student/examQuestions/${examId}`) ;
}
submitExam(submitionExam:ISubmitionExam):Observable<any> {
  return this.http.post<any>(`${this.baseUrl}Student/submitExam`, submitionExam) ;
}
}
