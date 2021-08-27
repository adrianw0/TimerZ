import { MatDialog } from "@angular/material/dialog";
import { LabelsService } from "./../Services/Labels.service";
import { Label } from "./../Shared/Models/Label";
import { MatTableDataSource } from "@angular/material/table";
import { LabelsDialogComponent } from "./LabelsDialog/LabelsDialog.component";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-Labels",
  templateUrl: "./Labels.component.html",
  styleUrls: ["./Labels.component.css"],
})
export class LabelsComponent implements OnInit {
  columndefs = ["name", "options"];

  labels: MatTableDataSource<Label>;

  labelName: string;

  constructor(public LabelsService: LabelsService, public dialog: MatDialog) {}

  ngOnInit() {
    this.refreshData();
  }

  private refreshData() {
    this.LabelsService.getLabels().subscribe(
      (result) => (this.labels = new MatTableDataSource<Label>(result))
    );
  }
  public deleteRow(id: number) {
    this.LabelsService.deleteLabel(id).subscribe(() => this.refreshData());
  }

  OpenLabelCreationDialog(): void {
    const dialogRef = this.dialog.open(LabelsDialogComponent, {
      width: "250px",
      data: { name: this.labelName },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result! in window) return;
      var label = new Label();
      label.name = result;

      this.LabelsService.addLabel(label).subscribe(
        (result) => {
          // this.labels.data.push(label);
          this.refreshData();
        },
        (err) => {
          alert(err.status);
        }
      );
    });
  }
}
