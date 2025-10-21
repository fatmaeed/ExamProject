import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGetAllExam } from '../../Models/iget-all-exam';


@Injectable({
  providedIn: 'root'
})
export class ExamService {
  baseUrl : string ="https://localhost:7085/api/"
  apiUrl: string = 'https://localhost:7085/api/Exam';
  constructor(private http:HttpClient) { }

  gitAllExam(): Observable<IGetAllExam []>{
    return this.http.get<IGetAllExam[]>(this.apiUrl);
  }
  gitById(id:number):Observable<IGetAllExam>{
    return this.http.get<IGetAllExam>(`${this.apiUrl}/Id?examId=${id}`);
  }
  gEtExam(id:number):Observable<IExamInfo>{
    return this.http.get<IExamInfo>(`${this.apiUrl}/Id?examId=${id}`);
  }

  getExamWithQuestions(examId:number): Observable<any>{
  return this.http.get<any>(`${this.apiUrl}/ExamWithQuestions/${examId}`);
}

search(examname:string):Observable<IGetAllExam []>{
  return this.http.get<IGetAllExam[]>(`${this.apiUrl}/Search/ExamName?examName=${examname}`)
}
  create(data: any):Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  update(id: number, exam: any):Observable<any> {
    return this.http.put(`${this.apiUrl}/id?examid=${id}`, exam);
  }

  delete(id: number):Observable<any> {
    return this.http.delete(`${this.apiUrl}?id=${id}`);
  }
  GetStudentReports(examId:number , page:number = 1 , size :number = 10) : Observable<IExamReports> {
    return this.http.get(`${this.baseUrl}Admin/Exams/${examId}/Students`)  as  Observable<IExamReports>;
  }
}
