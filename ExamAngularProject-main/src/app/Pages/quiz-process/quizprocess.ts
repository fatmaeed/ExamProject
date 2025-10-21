import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { Component, OnInit, OnDestroy, ViewChild, inject, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { QuizQuestion } from '../../Models/quiz-question';
import { QuizService } from '../../Services/QuizServices/quiz.service';
import { ISubmitionExam } from '../../Models/isubmition-exam';
import { ActivatedRoute } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { IJWTClaims } from '../../Components/models/ijwtclaims';

@Component({
  selector: 'app-quizprocess',
  standalone: true,
  templateUrl: './quizprocess.html',
  styleUrls: ['./quizprocess.css'],
  imports: [
    CommonModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatButtonModule,
    ReactiveFormsModule,
  ]
})
export class Quizprocess implements OnInit, OnDestroy {
  constructor(private cdr: ChangeDetectorRef, private route: ActivatedRoute) { }

  @ViewChild('stepper') stepper!: MatStepper;

  questions: QuizQuestion[] = [];
  answerForms: FormGroup[] = [];
  quizCompleted = false;
  score = 0;
  userAnswers: { isCorrect: boolean, selectedAnswer: number }[] = [];
  unansweredIndices: number[] = [];
  timeLeft = 60;
  timerInterval: any;
  timeOver = false;
  userId!: string;
  examId!: string;

  private fb = inject(FormBuilder);
  private quizService = inject(QuizService);

  getOptionLetter(index: number): string {
    return String.fromCharCode(65 + index);
  }
ngOnInit() {
  this.examId = this.route.snapshot.paramMap.get('examId') || '';
  const token = localStorage.getItem('token');
  if (token) {
    const decodedToken = jwtDecode<IJWTClaims>(token);
    this.userId = decodedToken.id;
  }

  this.quizService.getExamQuestions(this.examId).subscribe({
    next: (data) => {
      if (data && data.length > 0) {
        setTimeout(() => {
          this.questions = data.map((element: any) => ({
            questionText: element.questionText,
            questionId: element.questionId,
            score: element.score,
            options: [element.choice1, element.choice2, element.choice3, element.choice4],
            correctAnswer: ''
          }));

          this.buildForm();
          this.startTimer();
          this.cdr.detectChanges();
        });
      }
    },
    error: (err) => {
      console.log(err);
    }
  });
}


  ngOnDestroy() {
    clearInterval(this.timerInterval);
  }

  buildForm() {
    this.answerForms = this.questions.map(() =>
      this.fb.group({
        answer: ['', Validators.required]
      })
    );
  }

submitQuiz() {
  this.score = 0;
  this.userAnswers = [];
  this.unansweredIndices = [];

  let submitionExam: ISubmitionExam = {
    userId: +this.userId,
    examId: +this.examId,
    answers: []
  };

  this.questions.forEach((q, i) => {
    const selected = this.answerForms[i].value.answer;
    submitionExam.answers.push({ questionId: q.questionId, selectedAnswer: selected });

    // ✅ Disable the control here
    this.answerForms[i].get('answer')?.disable();
  });

  this.quizService.submitExam(submitionExam).subscribe({
    next: (data) => {
      this.questions.forEach((ques, i) => {
        ques.correctAnswer = data.details[i].correctAnswer;
      });
      this.score = data.totalScore;
    },
    error: (err) => {
      console.log("error in submition exam ", err);
    }
  });

  this.quizCompleted = true;
  clearInterval(this.timerInterval);

  setTimeout(() => {
    if (this.stepper) {
      this.stepper.selectedIndex = this.questions.length;
    }
      this.cdr.detectChanges();   

  });
}



  startTimer() {
    this.timerInterval = setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        this.timeOver = true;
        this.submitQuiz();
      }
    }, 1000);
  }

  getResultMessage(): string {
    const percentage = (this.score / this.questions.length) * 100;
    if (percentage >= 80) return 'Excellent work! You really know your stuff!';
    if (percentage >= 60) return 'Good job! You have a solid understanding.';
    if (percentage >= 40) return 'Not bad! Keep learning and improving.';
    return 'Keep practicing! You\'ll get better with time.';
  }

  trackByQuestionId(index: number, question: QuizQuestion): number {
    return question.questionId;
  }

  calculateTotalScoreOfExam() {
    return this.questions.reduce((total, question) => total + question.score!, 0);
  }
}
