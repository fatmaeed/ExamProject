export interface CompletedExam {
  examName: string;
  maxMarks: number;
  totalScore: number;
  isPassed: boolean;
  questions: {
    id: number;
    questionText: string;
    choice1: string;
    choice2: string;
    choice3: string;
    choice4: string;
    correctAnswer: number;
    score: number;
    answerScore: number;
    isCorrect: boolean;
  }[];
}
