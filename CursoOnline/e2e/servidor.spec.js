import { Selector } from "testcafe";
import Page from "./pagesModel/page";

const page = new Page();

fixture("Servidor")
    .page(page.urlBase);

test("Validando se está de pé", async t => {
    await t.expect(page.tituloDaPagina.innerText).eql("Cursos");
});