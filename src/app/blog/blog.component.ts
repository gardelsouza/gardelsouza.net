import { Component, OnInit } from '@angular/core';
import { BlogService } from '../services/blog.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

  posts = [];

  constructor(private blogService: BlogService) { }

  ngOnInit(): void {
    this.blogService.get5Posts().subscribe(data => {
      this.posts = data;
    }, error => {
      console.log('Não foi possível carregar os posts, vou dar uma avaliada no que aconteceu, volte mais tarde.')
    });
  }

}
