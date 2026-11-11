import login from "./auth.service";
import users from "./users/users";

export default function registerAllHandlers() {
    login();
    users();
}