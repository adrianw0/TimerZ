import { Project } from "./../../Shared/Models/Project";
import { Inject } from "@angular/core";
import { ProjectsComponent } from "./../Projects.component";
import { Component, OnInit } from "@angular/core";
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from "@angular/material/dialog";

@Component({
  selector: "app-ProjectDialog",
  templateUrl: "./ProjectDialog.component.html",
  styleUrls: ["./ProjectDialog.component.css"],
})
export class ProjectDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ProjectsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Project
  ) {}

  ngOnInit() {}
}
