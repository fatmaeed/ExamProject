import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Add this import
import { ExamService } from '../../Services/ExamService/exam-service';
import { LoadingSpinner } from "../../Components/loading-spinner/loading-spinner";
import { delay } from 'rxjs';

@Component({
  selector: 'app-exam-report',
  templateUrl: './exam-report.html',
  styleUrls: ['./exam-report.css'],
  standalone: true, // Add if using standalone components
  imports: [FormsModule, CommonModule, LoadingSpinner] // Add this for ngModel
 // Add this for ngModel
})
export class ExamReport implements OnInit {
  constructor(private examService: ExamService ,
    private activatedRoute: ActivatedRoute,
  private changeDetectorRef: ChangeDetectorRef
  ) { }
  isLoaded: boolean = false;
  exam!: IExamReports;
  currentDate: Date = new Date();
  filteredStudents: any[] = [];

  ngOnInit() {
    this.examService.GetStudentReports( this.activatedRoute.snapshot.params['id']).pipe(delay(500)).subscribe(
     {
       next: (response) => {

        this.exam = response;
   
        this.isLoaded = true;
        this.changeDetectorRef.detectChanges();
       },
       error: (error) => {
         console.log(error);
       }
     }
    );

    this.filteredStudents = [...this.exam.students];
  }

 


  calculatePercentage(marks: number): number {
    return Math.round((marks / this.exam.maxMarks) * 100);
  }


 }