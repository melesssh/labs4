Cypress.on("uncaught:exception", (err, runnable) => {
    return false;
  });

describe('task 1', ()=>{
    beforeEach(()=>{
        cy.viewport('macbook-16');
        cy.visit('https://allo.ua/');
    });
    it('should find product, buy product and see product in backet', ()=>{
        cy.get('#search-form__input').type('xiaomi redmi note 9 pro');
        cy.get('#__layout > div > div.v-header.show-overlay--below > div.main-header-wrapper > div.main-header-third-line-wrapper > div > ul > li.main-header__item.searchbar.searchbar--wider > div > div.search__form.search-form.search-form--results-ready > form > button.search-form__submit-button.search-form__submit-button--active').click();
        cy.get('#__layout > div > div:nth-child(2) > h1').should('contain','Результати пошуку для \'xiaomi redmi note 9 pro\'')
        cy.get('#__layout > div > div:nth-child(2) > div.v-catalog > div.v-catalog__content > div.v-catalog__products > div.products-layout__container.products-layout--grid > div:nth-child(1) > div > div.product-card__content > a').should('contain','Xiaomi Redmi Note 9 Pro 6/128GB Tropical Green')
        cy.get('#__layout > div > div:nth-child(2) > div.v-catalog > div.v-catalog__content > div.v-catalog__products > div.products-layout__container.products-layout--grid > div:nth-child(1) > div > div.product-card__content > div.product-card__buy-box > button').click();
        cy.get('.product-item__wrap').should('be.visible')
        cy.get('.wrap').should('contain','Xiaomi Redmi Note 9 Pro 6/128GB Tropical Green')
    });
   
})