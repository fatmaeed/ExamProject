import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../../Services/quizdata';
import { CompletedExam } from '../../Models/completed-exam';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-completed-exam-details',
  imports:[CommonModule],
  templateUrl: './completed-exam-details.html',
  styleUrls: ['./completed-exam-details.css']
})
export class CompletedExamDetails implements OnInit {
  examDetails: CompletedExam | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private quizService: QuizService,
      private cdr: ChangeDetectorRef

  ) {}

  ngOnInit(): void {
    const examId = this.route.snapshot.paramMap.get('id');
    console.log('Exam ID:', examId); 

    if (examId) {
      this.quizService.getCompletedExamsById(examId).subscribe({
        next: (data) => {
          console.log('API Response:', data);
          this.examDetails = data;
              this.cdr.detectChanges();

        },
        error: (err) => console.error('API error:', err)
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/profile']);
  }
}
