import { Project } from './Project';
import { Label } from './Label';

export class TimerEntry {
  id: number;
  description: string;
  startDate: Date;
  endDate: Date;
  elapsed: number;
  labels: Label[];
  project: Project;
  state: TimerState;

  constructor() {
    this.id = 0;
    this.description = '';
    this.startDate = null;
    this.endDate = null;
    this.elapsed = 0;
    this.labels = [];
    this.project = null;
    this.state = TimerState.new;

  }



}

export enum TimerState{
  new,
  Running,
  Finished
}
