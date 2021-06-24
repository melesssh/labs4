Cypress.on("uncaught:exception", (err, runnable) => {
   return false;
});

describe('angular example todo testing', () => {
   beforeEach(() => {
      cy.viewport('macbook-16');
      cy.visit('https://todomvc.com/examples/angularjs/#/');

   })
   it('should add new task to todo', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > section > ul > li > div > label').should('contain', '1');
   });


   it('new created task should  be listed in Active', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > footer > ul > li:nth-child(2) > a').click();
      cy.get('body > ng-view > section > section > ul > li > div > label').should('contain', '1');
   });

   it('new created task should not be listed in Completed', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > footer > ul > li:nth-child(3) > a').click();
      cy.get('body > ng-view > section > section > ul > li > div > label').should('not.exist');
   });

   it('completed task should not be listed in Active', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('.toggle').click();
      cy.get('body > ng-view > section > footer > ul > li:nth-child(2) > a').click();
      cy.get('body > ng-view > section > section > ul > li > div > label').should('not.exist');
   });

   it('active and complete tasks should be listed in All', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('.toggle').click();
      cy.get('body > ng-view > section > header > form > input').type('task2{enter}');
      cy.get('body > ng-view > section > footer > ul > li:nth-child(1) > a').click();
      cy.get('.completed > .view > .ng-binding').should('be.visible');
      cy.get(':nth-child(2) > .view > .ng-binding').should('be.visible');
   });


   it('should clear completed tasks', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('.toggle').click();
      cy.get('body > ng-view > section > footer > ul > li:nth-child(3) > a').click();
      cy.get('body > ng-view > section > footer > button').click();
      cy.get('body > ng-view > section > section > ul > li > div > label').should('not.exist');
   });


   it('left tasks cpounter should increase due to new task adding', () => {
      cy.get('.filters').should('not.exist');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('strong.ng-binding').should('contain','1');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('strong.ng-binding').should('contain','2');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('strong.ng-binding').should('contain','3');
   });

   it('left tasks cpounter should decrease due to task marking as completed', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('strong.ng-binding').should('contain','3');
      cy.get(':nth-child(1) > .view > .toggle').click();
      cy.get('strong.ng-binding').should('contain','2');
      cy.get(':nth-child(2) > .view > .toggle').click();
      cy.get('strong.ng-binding').should('contain','1');
      cy.get(':nth-child(3) > .view > .toggle').click();
      cy.get('strong.ng-binding').should('contain','0');
   });

   it('todo should be editable', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get(':nth-child(1) > .view > .ng-binding').dblclick()
      cy.get('.edit').type('edit{enter}');
      cy.get('.view > .ng-binding').should('contain','task1edit');
   });

   it('should mark all buttons as completed with "toggle all" button', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('strong.ng-binding').should('contain','3');
      cy.get('[for="toggle-all"]').click();
      cy.get('strong.ng-binding').should('contain','0');
   });

   it('should remove task from todo', () => {
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('body > ng-view > section > header > form > input').type('task1{enter}');
      cy.get('strong.ng-binding').should('contain','3');
      cy.get('body > ng-view > section > section > ul > li:nth-child(2) > div > button').click({force:true});
      cy.get('strong.ng-binding').should('contain','2');
   });

});