import { Routes } from '@angular/router';
import { ExamForm } from './Pages/exam-form/exam-form';
import { ExamList } from './Pages/exam-list/exam-list';
import { ExamQuestions } from './Pages/exam-questions/exam-questions';
import { AddQuestion } from './Pages/add-question/add-question';
import { UpdateQuestion } from './Pages/update-question/update-question';
import { ExamReport } from './Pages/exam-report/exam-report';
import { Register } from './Components/Account/register/register';
import { Login } from './Components/Account/login/login';
import { ConfirmEmail } from './Components/Account/confirm-email/confirm-email';
import { AuthGuard } from './Components/Account/AuthGaurd';
import { ChangePassword } from './Components/Account/change-password/change-password';
import { ForgotPassword } from './Components/Account/forgot-password/forgot-password';
import { ResetPassword } from './Components/Account/reset-password/reset-password';
import { Home } from './Pages/home/home';
import { NotFound } from './Pages/not-found/not-found';
import { AllQuizzes } from './componantsRahma/all-quizzes/all-quizzes';
import { Quizprocess } from './Pages/quiz-process/quizprocess';
import { Profile } from './componantsRahma/profile/profile';
import { CompletedExamsComponant } from './componantsRahma/completed-exams-componant/completed-exams-componant';
import { Quizzes } from './componantsRahma/quizzes/quizzes';
import { HowToPlayComponent } from './componantsRahma/how-to-play/how-to-play';
import { CompletedExamDetails } from './componantsRahma/completed-exam-details/completed-exam-details';

export const routes: Routes = [
    { path: "examform", component: ExamForm },
    { path: "examform/:id", component: ExamForm },
    { path: 'examlist', component: ExamList }
    , {
        path: "exam/:id/questions", component: ExamQuestions
    }, {
        path: "exam/:id/questions/create", component: AddQuestion
    }, {
        path: "exam/:id/questions/update/:questionId", component: UpdateQuestion
    }, {
        path: "exam/:id/report", component: ExamReport
    }, { path: 'account/register', component: Register },
    { path: 'account/login', component: Login },
    { path: 'Account/ConfirmEmail', component: ConfirmEmail },
    { path: 'account/forgotPassword', component: ForgotPassword },
    { path: 'Account/ResetPassword', component: ResetPassword },
    { path: 'account/changepassword', component: ChangePassword},
    {path:'quizProcess/:examId', component:Quizprocess},
    { path: 'home', component: Home },
    {path:'notFound', component:NotFound},
    {path:'allQuizzes', component:AllQuizzes  },
        {path:'profile', component:Profile  },
  { path: 'completedExam/:examId', component: CompletedExamsComponant },

    { path: '', redirectTo: 'home', pathMatch: 'full' }
   ,
    {path:"quizSection",component:Quizzes},
    {path:"howToPlay",component:HowToPlayComponent},

  { path: 'examDetails/:id', component: CompletedExamDetails }
   , {
        path: '**',
        redirectTo: 'notFound',
        pathMatch: 'full'
    }

];



