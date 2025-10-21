import { Component, OnInit } from '@angular/core';
import { CompletedExam } from '../../Models/completed-exam';
import { QuizService } from '../../Services/quizdata';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-completed-exams-componant',
  imports: [CommonModule],
  templateUrl: './completed-exams-componant.html',
  styleUrl: './completed-exams-componant.css'
})
export class CompletedExamsComponant implements OnInit {
  exam!: CompletedExam;

  constructor(private route: ActivatedRoute, private quizService: QuizService) {}

  ngOnInit(): void {
    const examId = this.route.snapshot.paramMap.get('examId');
    if (examId) {
      this.quizService.getCompletedExamsById(examId).subscribe({
        next: (data) => (this.exam = data),
        error: (err) => console.error('Failed to load completed exam', err)
      });
    }
  }
}

