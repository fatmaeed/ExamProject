import { IAnswerQuestion } from "./ianswer-question";

export interface ISubmitionExam {
  examId: number;
  userId: number;
  answers: IAnswerQuestion[];
}
