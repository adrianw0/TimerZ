import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Label } from '../Shared/Models/Label';

@Injectable()
export class LabelsService {


  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string)
  {
  }
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  public getLabels() : Observable<Label[]> {
    return this.http.get<Label[]>(this.baseUrl + "api/Labels");
  }
  public addLabel(label: Label) : Observable<Label> {
    var req = this.http.post<Label>(this.baseUrl +"api/AddLabel", JSON.stringify(label), this.httpOptions);
    return req;
  }
  deleteLabel(id: number) {
    var req = this.http.delete(this.baseUrl + "api/DeleteLabel/"+id)
    return req;
  }
}
