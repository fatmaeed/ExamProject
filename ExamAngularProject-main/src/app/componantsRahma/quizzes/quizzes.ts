import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { CommonModule } from '@angular/common';
import { QuizCardComponent } from "../../customs/quiz-card/quiz-card";
import { QuizService } from '../../Services/quizdata';
import { INewQuizModel } from '../../Models/inew-quiz-model';

@Component({
  selector: 'app-quizzes',
  imports: [CommonModule, QuizCardComponent],
  templateUrl: './quizzes.html',
  styleUrl: './quizzes.css'
})
export class Quizzes implements OnInit {
  quizzes: INewQuizModel[] = [];

  constructor(private router: Router, private quizService: QuizService,private cdr:ChangeDetectorRef) {

  }
  ngOnInit(): void {
        this.quizService.getQuizzes().subscribe((data) => {

          this.quizzes = data;
          this.cdr.detectChanges();
          console.log( data);
        console.log( this.quizzes);
    });
  }


  onImgError(event: Event) {
    (event.target as HTMLImageElement).src = 'assets/images/image.png';
  }


  navigateToAllQuizzes(): void {
  this.router.navigate(['allQuizzes']);
}
}



