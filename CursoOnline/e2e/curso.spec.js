import { Selector } from "testcafe";
import Curso from "./pagesModel/curso";

const curso = new Curso();

fixture("Curso")
    .page(curso.url);

test("Deve criar um novo curso", async t => {
    await t 
        .typeText(curso.inputNome, `Curso TestCafe - ${Date.now().toString()}`)
        .typeText(curso.inputCargaHoraria, "2000")
        .click(curso.selectPublicoAlvo)
        .click(curso.opcaoEmpregado)
        .click(curso.tipoOnline)
        .typeText(curso.inputValor, "5000");

    await t.click(curso.salvar);

    await t.expect(curso.tituloDaPagina.innerText).eql("Cursos");
});

test("Deve validar campos obrigatórios", async t => {
    await t.click(curso.salvar);

    await t
        .expect(curso.toastMessage.withText("Nome inválido").count).eql(1)
        .expect(curso.toastMessage.withText("Valor inválido").count).eql(1)
        .expect(curso.toastMessage.withText("Carga horária inválida").count).eql(1)
});