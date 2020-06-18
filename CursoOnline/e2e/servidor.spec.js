import { Selector } from "testcafe";

fixture("Servidor")
    .page("localhost:3000");

test("Validando se está de pé", async t => {
    await t.expect(Selector("title").innerText).eql("Cursos");
});