import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Project } from "../Shared/Models/Project";

@Injectable()
export class ProjectsService {

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string)
  {
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  public getProjects() : Observable<Project[]> {
    return this.http.get<Project[]>(this.baseUrl + "api/Projects");
  }
  public addProject(project: Project) : Observable<Project> {
    var req = this.http.post<Project>(this.baseUrl +"api/AddProject", JSON.stringify(project), this.httpOptions);
    return req;
  }

  DeleteProject(id: number) {
    var req = this.http.delete(this.baseUrl + "api/DeleteProject/"+id)
    return req;
  }
}
