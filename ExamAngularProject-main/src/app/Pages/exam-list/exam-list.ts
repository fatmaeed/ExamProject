import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ExamService } from '../../Services/ExamService/exam-service';
import { IGetAllExam } from '../../Models/iget-all-exam';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-exam-list',
  imports: [FormsModule,RouterLink , CommonModule],
  templateUrl: './exam-list.html',
  styleUrl: './exam-list.css'
})
export class ExamList implements OnInit {

exams: IGetAllExam[] = [];
  datatoday:Date = new Date();
  searchKeyword!:string;
  constructor(private examService : ExamService,private cdr:ChangeDetectorRef){}
  ngOnInit(): void {

    this.examService.gitAllExam().subscribe({
      next:(response)=>{
        this.exams = response;
        this.cdr.detectChanges();
        console.log(this.exams)

      }  ,
      error:(error)=>{
        console.log(error);
      }
    })
  }

  Search(){
    if(this.searchKeyword == ""){
      this.examService.gitAllExam().subscribe({
        next:(resp)=>{
          this.exams = resp;
          this.cdr.detectChanges();
        },
        error:(err)=> {
            console.log(err)
        }
      })
    }else
      this.examService.search(this.searchKeyword).subscribe({
        next:(resp)=>{
          console.log(resp)
          this.exams = resp;
          this.cdr.detectChanges();
        },
        error:(err)=> {
            console.log(err)
        }
      })
  }

  isEnded(endTime: string|Date): boolean {
  return new Date(endTime) <= this.datatoday;
}
  deleteExam(id:number){
    console.log(id)
    var flag = confirm("Are you sure delete this Exam?");
    if(flag){
      this.examService.delete(id).subscribe({
      next:()=>{
      this.exams = this.exams.filter(exam => exam.id !== id);
       this.cdr.detectChanges();
      },
      error:(error)=>{
         alert("Delete failed.")
      }
    })
    }

  }

}
