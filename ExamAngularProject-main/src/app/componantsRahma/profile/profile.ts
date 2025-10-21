import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { QuizService } from '../../Services/quizdata';
import { AccountService } from '../../Components/services/account-service';
import { Router } from '@angular/router';
import { QuizCardComponent } from "../../customs/quiz-card/quiz-card";
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ CommonModule],
  templateUrl: './profile.html',
  styleUrls: ['./profile.css']
})
export class Profile implements OnInit {
  completedExams: any[] = [];

  constructor(
    private quizService: QuizService,
    public accountService: AccountService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    if (this.accountService.isLoggedIn()) {
      this.quizService.getCompletedExams().subscribe({
        next: (data) => {
          this.completedExams = data;
          this.cdr.detectChanges();
        },
        error: (err) => console.error('Error fetching exams:', err),
      });
    }
  }

 openCompletedExam(examId: number): void {
  if (!examId) {
    console.warn('Invalid exam ID:', examId);
    return;
  }
  this.router.navigate(['examDetails', examId]);
  console.log('Exam ID from route:', examId);

}

}
