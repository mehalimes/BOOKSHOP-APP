import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { FunctionsService } from '../../services/functions.service';
import { SharedStateService } from '../../services/shared-state.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-comment',
  imports: [CommonModule],
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.css'
})
export class CommentComponent {
  @Input() id: number = 0;
  @Input() username: string = "";
  @Input() email: string = "";
  @Input() bookId: number = 0;
  @Input() content: string = "";
  canUserDelete: boolean = false;

  constructor(
    private http: HttpClient,
    private functions: FunctionsService,
    private sharedState: SharedStateService
  ) { }

  ngOnInit() {
    this.canUserDelete = this.email == localStorage.getItem("email");
  }

  async removeComment() {
    try {
      const removeCommentResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/removeAComment",
          { commentId: this.id },
          { responseType: 'text' as 'json' }
        )
      );
      console.log(removeCommentResponse);
      const serviceResponse = await this.functions.updateAllComments(this.bookId);
      this.sharedState.allComments = serviceResponse;
    } catch (err) {
      console.log(err);
    };
  }
}


