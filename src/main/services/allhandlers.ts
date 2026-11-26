import login from "./auth.service";
import products from "./products/product";
import users from "./users/users";

export default function registerAllHandlers() {
    login();
    users();
    products();
}