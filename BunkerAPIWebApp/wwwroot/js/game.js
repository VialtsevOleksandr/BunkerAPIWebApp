// Функція для отримання параметрів запиту з URL
function getQueryParam(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(window.location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}

// Отримуємо ідентифікатор гри з URL
var gameId = getQueryParam('id');

// Змінюємо заголовок сторінки, щоб він включав ідентифікатор гри
document.title = 'Гра №' + gameId;
var pageTitle = document.getElementById('page-title');
pageTitle.textContent = 'Гра №' + gameId;

// Тепер ви можете використати gameId для отримання даних про гру
async function getGameDetails() {
    try {
        var gameSettingResponse = await fetch('api/GameSettings/' + gameId);
        var gameSetting = await gameSettingResponse.json();

        var catastropheResponse = await fetch('api/Catastrophes/' + gameSetting.catastropheId);
        var catastrophe = await catastropheResponse.json();
        document.getElementById('catastropheName').textContent = catastrophe.catastropheName;

        var bunkerResponse = await fetch('api/Bunkers/' + gameSetting.bunkerId);
        var bunker = await bunkerResponse.json();

        var bunkerSizeResponse = await fetch('api/BunkerSizes/' + bunker.bunkerSizeId);
        var bunkerSize = await bunkerSizeResponse.json();
        document.getElementById('bunkerSize').textContent = bunkerSize.size;

        var residenceTimeResponse = await fetch('api/ResidenceTimes/' + bunker.residenceTimeId);
        var residenceTime = await residenceTimeResponse.json();
        document.getElementById('residenceTime').textContent = residenceTime.residenceTimeName;

        var foodQuantityResponse = await fetch('api/FoodQuantities/' + bunker.foodQuantityId);
        var foodQuantity = await foodQuantityResponse.json();
        document.getElementById('foodQuantity').textContent = foodQuantity.foodQuantityName;

        var bunkerInventoryResponse = await fetch('api/BunkerInventories/' + bunker.bunkerInventoryId);
        var bunkerInventory = await bunkerInventoryResponse.json();
        document.getElementById('bunkerInventory').textContent = bunkerInventory.bunkerInventoryName;
    } catch (error) {
        console.error('Error:', error);
    }
}

getGameDetails();

async function getPlayerCards() {
    try {
        var gameSettingResponse = await fetch('api/GameSettings/' + gameId);
        var gameSetting = await gameSettingResponse.json();
        var cardNumber = 1;

        gameSetting.gameSettingHumanCards.forEach(async (gameSettingHumanCard) => {
            var humanCardResponse = await fetch('api/HumanCards/' + gameSettingHumanCard.humanCardId);
            var humanCard = await humanCardResponse.json();

            var additionalInformationResponse = await fetch('api/AdditionalInformations/' + humanCard.additionalInformationId);
            var additionalInformation = await additionalInformationResponse.json();

            var genderResponse = await fetch('api/Genders/' + humanCard.genderId);
            var gender = await genderResponse.json();

            //var genderTypeResponse = await fetch('api/GenderTypes/' + gender.genderTypeId);
            //var genderType = await genderTypeResponse.json();

            var healthResponse = await fetch('api/Healths/' + humanCard.healthId);
            var health = await healthResponse.json();

            var hobbyResponse = await fetch('api/Hobbies/' + humanCard.hobbyId);
            var hobby = await hobbyResponse.json();

            var humanTraitResponse = await fetch('api/HumanTraits/' + humanCard.humanTraitId);
            var humanTrait = await humanTraitResponse.json();

            var inventoryResponse = await fetch('api/Inventories/' + humanCard.inventoryId);
            var inventory = await inventoryResponse.json();

            var phobiaResponse = await fetch('api/Phobias/' + humanCard.phobiaId);
            var phobia = await phobiaResponse.json();

            var professionResponse = await fetch('api/Professions/' + humanCard.professionId);
            var profession = await professionResponse.json();

            var specialFeatureResponse = await fetch('api/SpecialFeatures/' + humanCard.specialFeatureId);
            var specialFeature = await specialFeatureResponse.json();

            var cardElement = document.createElement('div');
            cardElement.id = 'playerCard' + gameSettingHumanCard.humanCardId;
            cardElement.className = 'player-card';
            cardElement.innerHTML = `
                <h3 style="text-align: center;">Карта гравця №${cardNumber}</h3>
                <p><strong>Професія:</strong> <span class="profession">${profession.professionName}</span></p>
                <p><strong>Досвід роботи:</strong> <span class="work-experience">${profession.workExperience}</span></p>
                <p><strong>Стать:</strong> <span class="gender">${gender.genderTypeId == '1' ? 'Чоловік' : 'Жінка'}</span></p>
                <p><strong>Вік:</strong> <span class="age">${gender.age}</span></p>
                <p><strong>Чи може мати дитину:</strong> <span class="is-childfree">${gender.isChildfree ? 'Ні' : 'Так'}</span></p>
                <p><strong>Стан здоров'я:</strong> <span class="health">${health.healthStatus}</span></p>
                ${health.diseaseStage ? `<p><strong>Стадія хвороби:</strong> <span class="disease-stage">${health.diseaseStage}</span></p>` : ''}
                <p><strong>Хобі:</strong> <span class="hobby">${hobby.hobbyName}</span></p>
                <p><strong>Риса характеру:</strong> <span class="human-trait">${humanTrait.traitName}</span></p>
                <p><strong>Інвентар:</strong> <span class="inventory">${inventory.inventoryName}</span></p>
                <p><strong>Фобія:</strong> <span class="phobia">${phobia.phobiaName}</span></p>
                <p><strong>Додаткова інформація:</strong> <span class="additional-info">${additionalInformation.additionalInformationName}</span></p>
                <p><strong>Карта дії:</strong> <span class="special-feature">${specialFeature.specialFeatureName}</span></p>
            `;

            var playerCardsWrapper = document.querySelector('.player-cards-wrapper');
            playerCardsWrapper.appendChild(cardElement);
            cardNumber++;
        });
    } catch (error) {
        console.error('Error:', error);
    }
}

getPlayerCards();

