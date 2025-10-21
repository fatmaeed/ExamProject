export interface QuizQuestion {
  questionId: number;
  questionText: string;
  correctAnswer: string;
  options: string[];
  score?:number
}
