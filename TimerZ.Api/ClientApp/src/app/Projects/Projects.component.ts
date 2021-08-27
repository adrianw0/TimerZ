import { ProjectDialogComponent } from "./ProjectDialog/ProjectDialog.component";
import { MatDialog } from "@angular/material/dialog";
import { MatTableDataSource } from "@angular/material/table";
import { ProjectsService } from "./../Services/Projects.service";
import { Project } from "./../Shared/Models/Project";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-Projects",
  templateUrl: "./Projects.component.html",
  styleUrls: ["./Projects.component.css"],
})
export class ProjectsComponent implements OnInit {
  columndefs = ["name", "options"];
  projects: MatTableDataSource<Project>;

  projectName: string;

  constructor(
    public projectsService: ProjectsService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.refreshData();
  }

  private refreshData() {
    this.projectsService
      .getProjects()
      .subscribe(
        (result) => (this.projects = new MatTableDataSource<Project>(result))
      );
  }
  public deleteRow(id: number) {
    this.projectsService.DeleteProject(id).subscribe(() => this.refreshData());
  }

  OpenProjectCreationDialog(): void {
    const dialogRef = this.dialog.open(ProjectDialogComponent, {
      width: "250px",
      data: { name: this.projectName },
    });

    dialogRef.afterClosed().subscribe((result) => {
      {
        if (result! in window) return;

        var project = new Project();
        project.name = result;

        this.projectsService.addProject(project).subscribe(
          (result) => {
            // this.projects.data.push(project);
            this.refreshData();
          },
          (err) => {
            alert(err.status);
          }
        );
      }
    });
  }
}
