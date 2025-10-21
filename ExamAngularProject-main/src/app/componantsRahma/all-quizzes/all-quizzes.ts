import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { QuizCardComponent } from '../../customs/quiz-card/quiz-card';
import { QuizService } from '../../Services/quizdata';
import { INewQuizModel } from '../../Models/inew-quiz-model';

@Component({
  selector: 'app-all-quizzes',
  standalone: true,
  imports: [CommonModule, QuizCardComponent],
  templateUrl: './all-quizzes.html',
  styleUrl: './all-quizzes.css'
})
export class AllQuizzes {
  quizzes: INewQuizModel[] = [];
  hues: number[] = [];

  constructor(
    private router: Router,
    private quizService: QuizService,
    private cdr: ChangeDetectorRef
  ) {
    this.quizService.getQuizzes().subscribe((data) => {
      this.quizzes = data;
      this.cdr.detectChanges();
    });
  }

  navigateToQuiz(): void {
    this.router.navigate(['quizScreen']);
  }


}
