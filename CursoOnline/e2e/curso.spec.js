import { Selector } from "testcafe";

fixture("Curso")
    .page("localhost:3000/Curso/Novo");

test("Deve criar um novo curso", async t => {
    await t 
        .typeText(Selector("[name='Nome']"), `Curso TestCafe - ${Date.now().toString()}`)
        .typeText(Selector("[name='CargaHoraria']"), "2000")
        .click(Selector("[name='PublicoAlvo']"))
        .click(Selector("option[value='Empregado']"))
        .click(Selector("[value='Online']"))
        .typeText(Selector("[name='Valor']"), "5000");

    await t.click(Selector(".btn-success"));

    await t.expect(Selector("title").innerText).eql("Cursos");
});