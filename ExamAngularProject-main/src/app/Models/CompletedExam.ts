import { QuizQuestion } from "./quiz-question";

export interface CompletedExam {
  id: number;
  examName: string;
  score: number;
  date: string;
  questions: QuizQuestion[];
}
