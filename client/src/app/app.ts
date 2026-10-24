

import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface AppUser {
  id: number;
  userName: string;
}

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);

  users = signal<AppUser[]>([]);
  loading = signal(true);
  error = signal('');

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.http
      .get<AppUser[]>('https://localhost:7116/api/users')
      .subscribe({
        next: response => {
          this.users.set(response);
          this.loading.set(false);
        },
        error: err => {
          console.error(err);
          this.error.set('Unable to load users from the API.');
          this.loading.set(false);
        }
      });
  }
}
