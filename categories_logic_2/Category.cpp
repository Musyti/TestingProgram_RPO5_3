#include "Category.h"


// Конструктор по умолчанию
Category::Category()
    : name_("Без названия")
    , questions_()
    , is_finished_(false)
    , is_active_(false)
    , test_description_("")
    , results_(){
}

// Конструктор с параметром
Category::Category(const std::string& name)
    : name_(name)
    , questions_()
    , is_finished_(false)
    , is_active_(false) 
    , test_description_ ("")
    , results_(){
}

Category::Category(const std::string& name, const std::string& test_description)
    : name_(name)
    , questions_()
    , is_finished_(false)
    , is_active_(false)
    , test_description_(test_description) {
}

// Деструктор
Category::~Category() {
    // Очистка ресурсов при необходимости
}

// Геттеры и сеттеры
std::string Category::getName() const {
    return name_;
}

void Category::setName(const std::string& name) {
    name_ = name;
}

std::string Category::getTestDescription() const {
    return test_description_;
}

void Category::setTestDescription(const std::string& test_description){
    test_description_ = test_description;
}

bool Category::isFinished() const {
    return is_finished_;
}

bool Category::isActive() const {
    return is_active_;
}

/**
 * Отображает информацию о категории
 */
void Category::showInfo() {
    std::cout << "=== Информация о категории ===" << std::endl;
    std::cout << "Название: " << name_ << std::endl;
    std::cout << "Количество вопросов: " << questions_.size() << std::endl;
    std::cout << "Статус: " << (is_active_ ? "Активна" : "Не активна") << std::endl;
    std::cout << "Завершена: " << (is_finished_ ? "Да" : "Нет") << std::endl;

    if (!questions_.empty()) {
        std::cout << "\nСписок вопросов:" << std::endl;
        for (size_t i = 0; i < questions_.size(); ++i) {
            std::cout << i + 1 << ". Вопрос " << i + 1 << std::endl;
        }
    }
}

/**
 * Запускает категорию
 */
void Category::start() {
    if (questions_.empty()) {
        std::cout << "Ошибка: В категории нет вопросов!" << std::endl;
        return;
    }

    is_active_ = true;
    is_finished_ = false;
    std::cout << "Категория \"" << name_ << "\" запущена!" << std::endl;
}

/**
 * Переходит к следующему вопросу
 */
void Category::nextQuestion() {
    if (!is_active_) {
        std::cout << "Ошибка: Категория не активна!" << std::endl;
        return;
    }

    if (currentQuestionIndex < static_cast<int>(questions_.size()) - 1) {
        currentQuestionIndex++;
        std::cout << "Переход к следующему вопросу" << std::endl;
    }
    else {
        std::cout << "Это был последний вопрос в категории" << std::endl;
    }
}

/**
 * Возвращается к предыдущему вопросу
 */
void Category::previousQuestion() {
    if (!is_active_) {
        std::cout << "Ошибка: Категория не активна!" << std::endl;
        return;
    }

    if (currentQuestionIndex > 0) {
        currentQuestionIndex--;
        std::cout << "Возврат к предыдущему вопросу" << std::endl;
    }
    else {
        std::cout << "Это первый вопрос в категории" << std::endl;
    }
}

/**
 * Завершает категорию и возвращает набранные очки
 */
int Category::end() {
    if (!is_active_) {
        std::cout << "Ошибка: Категория не была запущена!" << std::endl;
        return 0;
    }

    is_active_ = false;
    is_finished_ = true;

    // Подсчет набранных очков (упрощенная логика)
    int earnedPoints = 0; // Например, за все вопросы

    for (int i = 0; i < questions_.size(); i++) {
        earnedPoints += questions_.at(i).countPoints();
    }

    std::cout << "Категория \"" << name_ << "\" завершена!" << std::endl;
    std::cout << "Набрано очков: " << earnedPoints << std::endl;

    return earnedPoints;
}

/**
 * Добавляет новый вопрос в категорию
 */
void Category::addQuestion(const std::string& content,
    const std::vector<std::string>& options,
    const std::vector<int>& points_options,
    int points,
    const std::string& explanation) {
    questions_.push_back(Question(content, options, points_options));

    std::cout << "Новый вопрос добавлен в категорию \"" << name_ << "\"" << std::endl;
}

/**
 * Удаляет вопрос по индексу
 */
void Category::removeQuestion(int index) {
    if (index >= 0 && index < static_cast<int>(questions_.size())) {
        questions_.erase(questions_.begin() + index);
        std::cout << "Вопрос с индексом " << index << " удален" << std::endl;
    }
    else {
        std::cout << "Ошибка: Неверный индекс вопроса!" << std::endl;
    }
}

/**
 * Загружает категорию из файла
 */
json Category::toJson() const {
    json j;
    j["name"] = name_;
    j["is_finished"] = is_finished_;
    j["is_active"] = is_active_;
    j["test_description"] = test_description_;
    // Сохранение вопросов
    j["questions"] = json::array();
    j["results"] = json::array();
    for (const Question& question : questions_) {
        j["questions"].push_back(question.toJson()); // Предполагается, что у Question есть toJson()
    }
    for (const ResultRange& result : results_) {
        j["results"].push_back(result.toJson()); // Предполагается, что у ResultRange есть toJson()
    }
    return j;
}

/**
 * Загружает категорию из JSON объекта
 */
void Category::fromJson(const json& j) {
    try {
        name_ = j.at("name").get<std::string>();
        is_finished_ = j.value("is_finished", false);
        is_active_ = j.value("is_active", false);

        // Загрузка вопросов
        questions_.clear();
        if (j.contains("questions") && j["questions"].is_array()) {
            for (const auto& question_json : j["questions"]) {
                Question q;
                q.fromJson(question_json); // Предполагается, что у Question есть fromJson()
                questions_.push_back(q);
            }
        }
        results_.clear();
        if (j.contains("results") && j["results"].is_array()) {
            for (const auto& result_json : j["results"]) {
                ResultRange re;
                re.fromJson(result_json); // Предполагается, что у ResultRange есть fromJson()
                results_.push_back(re);
            }
        }
    }
    catch (const json::exception& e) {
        std::cerr << "Ошибка парсинга JSON: " << e.what() << std::endl;
        throw;
    }
}

/**
 * Сохраняет категорию в файл в формате JSON
 */
void Category::saveToFile(const std::string& filename) {
    try {
        // Создаем JSON объект
        json j = toJson();

        // Запись в файл с форматированием (отступ 4 пробела)
        std::ofstream file(filename);
        if (!file.is_open()) {
            throw std::runtime_error("Не удалось открыть файл для записи: " + filename);
        }

        file << j.dump(4); // Красивый вывод с отступами

        file.close();
        std::cout << "Категория \"" << name_ << "\" успешно сохранена в файл: "
            << filename << std::endl;
        std::cout << "   - Вопросов сохранено: " << questions_.size() << std::endl;

    }
    catch (const std::exception& e) {
        std::cerr << "Ошибка при сохранении в файл " << filename << ": "
            << e.what() << std::endl;
    }
}

/**
 * Загружает категорию из файла в формате JSON
 */
void Category::loadFromFile(const std::string& filename) {
    try {
        // Чтение файла
        std::ifstream file(filename);
        if (!file.is_open()) {
            throw std::runtime_error("Не удалось открыть файл: " + filename);
        }

        // Парсинг JSON
        json j;
        file >> j;
        file.close();

        // Загрузка данных
        fromJson(j);

        std::cout << "Категория успешно загружена из файла: " << filename << std::endl;
        std::cout << "   - Название: " << name_ << std::endl;
        std::cout << "   - Вопросов загружено: " << questions_.size() << std::endl;

    }
    catch (const std::exception& e) {
        std::cerr << "Ошибка при загрузке из файла " << filename << ": "
            << e.what() << std::endl;
    }
}

/**
 * Валидация JSON схемы
 */
bool Category::validateJsonSchema(const json& j) {
    // Проверка наличия обязательных полей
    if (!j.contains("name") || !j["name"].is_string()) {
        std::cerr << "Ошибка: Отсутствует поле 'name' или не является строкой" << std::endl;
        return false;
    }

    if (!j.contains("points") || !j["points"].is_number_integer()) {
        std::cerr << "Ошибка: Отсутствует поле 'points' или не является числом" << std::endl;
        return false;
    }

    // Проверка вопросов, если они есть
    if (j.contains("questions")) {
        if (!j["questions"].is_array()) {
            std::cerr << "Ошибка: Поле 'questions' должно быть массивом" << std::endl;
            return false;
        }
    }

    return true;
}