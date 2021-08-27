import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  title = "app";

  public isMenuOpen: boolean = false;

  public onSidenavClick(): void {
    this.isMenuOpen = false;
  }

  public pages: Page[] = [
    { name: 'Timers', link: 'Timers', icon: 'watch' },
    { name: 'Projects', link: 'Projects', icon: 'star' },
    { name: 'Lables', link: 'Labels', icon: 'star' },
    { name: 'Summary', link: 'Summary', icon: 'event_note' },
    { name: 'Calendar', link: 'Calendar', icon: 'calendar_today' },
    { name: 'Settings', link: 'Settings', icon: 'build' }
  ]
}


interface Page {
  link: string;
  name: string;
  icon: string;
}
